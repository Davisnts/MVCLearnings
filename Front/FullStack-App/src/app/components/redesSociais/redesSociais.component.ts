import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { RedeSocial } from '@app/models/RedeSocial';
import { RedeSocialService } from '@app/services/redeSocial.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-redesSociais',
  templateUrl: './redesSociais.component.html',
  styleUrls: ['./redesSociais.component.css'],
})
export class RedesSociaisComponent implements OnInit {
  modalRef: BsModalRef;
  @Input() eventoId = 0;
  public formRS: FormGroup;
  public redeSocialAtual = { id: 0, nome: '', indice: 0 };

  public get redesSociais(): FormArray {
    return this.formRS.get('redesSociais') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private redeSocialService: RedeSocialService,
    private modalService: BsModalService
  ) {}

  ngOnInit() {
    this.carregarRedesSociais(this.eventoId);
    this.validation();
  }

  public validation(): void {
    this.formRS = this.fb.group({
      redesSociais: this.fb.array([]),
    });
  }

  public carregarRedesSociais(id: number = 0): void {
    let origem = 'Palestrante';

    if (this.eventoId != 0) origem = 'evento';
    this.spinner.show();
    this.redeSocialService
      .getRedesSociais(origem, id)
      .subscribe({
        next: (RedeSocialsRetorno: RedeSocial[]) => {
          RedeSocialsRetorno.forEach((RedeSocial) => {
            this.redesSociais.push(this.createRedeSocial(RedeSocial));
          });
        },
        error: (error) => {
          this.toastr.error('Erro ao tentar recuperar RedeSocials!', 'Error!');
          console.error(error);
        },
      })
      .add(() => {
        this.spinner.hide();
      });
  }
  adicionarRedeSocial(): void {
    this.redesSociais.push(this.createRedeSocial({ id: 0 } as RedeSocial));
  }
  createRedeSocial(redeSocial: RedeSocial): FormGroup {
    return this.fb.group({
      id: [redeSocial.id],
      nome: [redeSocial.nome, Validators.required],
      url: [redeSocial.url, Validators.required],
    });
  }
  public returnTituloRedeSocial(nome: string) {
    return nome === null || nome == '' ? 'Nome da RedeSocial' : nome;
  }
  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }

  public salvarRedesSociais(): void {
    let origem = 'Palestrante';

    if (this.eventoId != 0) origem = 'evento';

    if (this.formRS.controls['redesSociais'].valid) {
      this.spinner.show();
      this.redeSocialService
        .salvarRedesSocais(origem, this.eventoId, this.formRS.value.redesSociais)
        .subscribe({
          next: () => {
            this.toastr.success(
              'redesSociais salvos com Sucesso!!',
              'Sucesso!!'
            );
          },
          error: (error: any) => {
            this.toastr.error('Erro ao tentar salvar redesSociais.', 'Error!');
            console.error(error);
          },
        })
        .add(() => this.spinner.hide());
    }
  }
  public removerRedeSocial(template: TemplateRef<any>, indice: number): void {
    this.redeSocialAtual.id = this.redesSociais.get(indice + '.id')?.value;
    this.redeSocialAtual.nome = this.redesSociais.get(indice + '.nome')?.value;
    this.redeSocialAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  public confirmDeleteRedeSocial(): void {
    this.spinner.show();
    this.modalRef?.hide();
    let origem = 'Palestrante';

    if (this.eventoId != 0) origem = 'evento';

    this.redeSocialService
      .deleteRedeSocial(origem, this.eventoId, this.redeSocialAtual.id)
      .subscribe({
        next: () => {
          this.toastr.success('RedeSocial apagado com sucesso', 'Sucesso!');
          this.redesSociais.removeAt(this.redeSocialAtual.indice);
        },
        error: (error) => {
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }
  public declineDeleteRedeSocial(): void {
    this.modalRef?.hide();
  }
}
