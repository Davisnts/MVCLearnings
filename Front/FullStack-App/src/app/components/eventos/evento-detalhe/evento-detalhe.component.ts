import { Component, TemplateRef } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { Lote } from '@app/models/Lote';
import { EventoService } from '@app/services/evento.service';
import { LoteService } from '@app/services/lote.service';
import { environment } from '@environments/environment';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent {
  modalRef?: BsModalRef;
  eventoId: any;
  evento = {} as Evento;
  form!: FormGroup;
  stateSave = 'addEvento';
  loteAtual = { id: 0, nome: '', indice: 0 };
  minDate: Date;
  ImagemUrl = 'assets/img/cloud.png';
  file!: File;

  get f(): any {
    return this.form.controls;
  }
  get modoEditar(): boolean {
    return this.stateSave === 'updateEvento';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }
  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm',
      showWeekNumber: false,
    };
  }
  get bsOnlyDay(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      showWeekNumber: false,
    };
  }
  
  
  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activerouter: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private eventoService: EventoService,
    private loteService: LoteService,
    private router: Router
  ) {
    this.minDate = new Date();
    this.minDate.setDate(this.minDate.getDate());
    this.localeService.use('pt-br');
  }

  public carregarEvento(): void {
    this.eventoId = this.activerouter.snapshot.paramMap.get('id');

    if (this.eventoId !== null && this.eventoId !== 0) {
      this.stateSave = 'updateEvento';
      this.eventoService
        .getEventoById(+this.eventoId)
        .subscribe({
          next: (evento: Evento) => {
            this.evento = { ...evento };
            this.form.patchValue(this.evento);
            console.log(evento);
            if(this.evento.imagemUrl !=''){
              this.ImagemUrl = environment.apiURL + "resources/Eventos/" + this.evento.imagemUrl;
            }
          
            this.carregarLotes();
          },
          error: (error: any) => {
            console.error(error);
          },
        })
        .add(() => {
          this.spinner.hide();
        });
    }
  }

  public carregarLotes(): void {
    this.loteService
      .getLoteByEventoId(this.eventoId)
      .subscribe({
        next: (lotesRetorno: Lote[]) => {
          lotesRetorno.forEach((lote) => {
            this.lotes.push(this.createLote(lote));
          });
        },
        error: (error) => {
          this.toastr.error('Erro ao tentar recuperar lotes!', 'Error!');
          console.error(error);
        },
      })
      .add(() => {
        this.spinner.hide();
      });
  }
  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
  }

  public validation(): void {
    this.form = this.fb.group({
      local: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(128),
        ],
      ],
      dataEvento: ['', Validators.required],
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(128),
        ],
      ],
      qtdPessoas: [
        '',
        [Validators.required, Validators.min(4), Validators.max(128000)],
      ],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemUrl: [''],
      lotes: this.fb.array([])
    });
  }
  adicionarLote(): void {
    this.lotes.push(this.createLote({ id: 0 } as Lote));
  }
  createLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, [Validators.required]],
      quantidade: [lote.quantidade, [Validators.required]],
      preco: [lote.preco, [Validators.required]],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim],
    });
  }
  public resetForm(): void {
    this.form.reset();
  }
public returnTituloLote(nome:string){
return nome === null || nome == '' ? 'Nome do lote' : nome;
}
  public cssValidator(campoForm: any): any {
    if (campoForm!= null) {
   return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
    }
  public salvarEvento(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.evento =
        this.stateSave === 'addEvento'
          ? (this.evento = { ...this.form.value })
          : { id: this.evento.id, ...this.form.value };
    }
    this.eventoService[this.stateSave](this.evento)
      .subscribe({
        next: (eventoreturn: Evento) => {
          this.toastr.success('Evento salvo com sucesso', 'Sucesso!');
          this.router.navigate([`eventos/detalhe/${eventoreturn.id}`]);
        },
        error: () => {
          console.error(Error);
          this.toastr.error('Error ao salvar evento', 'Error');
        },
      })
      .add(() => this.spinner.hide());
  }
  public salvarLotes(): void {
    if (this.form.controls['lotes'].valid) {
      this.spinner.show();
      this.loteService
        .updateLote(this.evento.id, this.form.value.lotes)
        .subscribe({
          next: () => {
            this.toastr.success('Lotes salvos com Sucesso!!', 'Sucesso!!');
            this.lotes.reset();
          },
          error: (error: any) => {
            this.toastr.error('Erro ao tentar salvar lotes.', 'Error!');
            console.error(error);
          },
        })
        .add(() => this.spinner.hide());
    }
  }
  public removerLote(template: TemplateRef<any>, indice: number): void {
    this.loteAtual.id = this.lotes.get(indice + '.id')?.value;
    this.loteAtual.nome = this.lotes.get(indice + '.nome')?.value;
    this.loteAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  public confirmDeleteLote(): void {
    this.spinner.show();
    this.modalRef?.hide();
    this.loteService
      .deleteLote(this.eventoId, this.loteAtual.id)
      .subscribe({
        next: () => {
          this.toastr.success('Lote apagado com sucesso', 'Sucesso!');
          this.lotes.removeAt(this.loteAtual.indice);
        },
        error: (error) => {
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }
  public declineDeleteLote(): void {
    this.modalRef?.hide();
  }
  onFileChange(ev:any): void{
    const reader = new FileReader();
    reader.onload= (event: any) => this.ImagemUrl = event.target.result
    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0])
    this.uploadImagem();
  }
  uploadImagem(): void{
    this.spinner.show;
    this.eventoService.postUpload(this.eventoId,this.file).subscribe({
      next:()=>{this.carregarEvento(); 
        this.toastr.success('Imagem autoalizado com sucesso','Sucesso!')},
      error:(error)=>{this.toastr.error('Erro ao autoalizar imagem','Error!');
      console.error(error);
      }


    }).add(()=>this.spinner.hide())
  }
}