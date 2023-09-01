import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {
  AbstractControlOptions,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ValidatorField } from '@app/helpers/ValidatorField';
import { UserUpdate } from '@app/models/Identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { PalestranteService } from '@app/services/palestrante.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-perfil-detalhe',
  templateUrl: './perfil-detalhe.component.html',
  styleUrls: ['./perfil-detalhe.component.css'],
})
export class PerfilDetalheComponent implements OnInit {
  @Output() changeFormValue = new EventEmitter();

  userUpdate = {} as UserUpdate;
  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }
  constructor(
    private fb: FormBuilder,
    public accountService: AccountService,
    public palestranteService: PalestranteService,
    private router: Router,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.validation();
    this.carregarUsuario();
    this.verifyForm();
  }

  private verifyForm(): void {
    this.form.valueChanges.subscribe({
      next: () => this.changeFormValue.emit({ ...this.form.value }),
    });
  }

  private carregarUsuario(): void {
    this.accountService.getUser().subscribe({
      next: (userRetorno: UserUpdate) => {
        console.log(userRetorno);
        this.userUpdate = userRetorno;
        this.form.patchValue(this.userUpdate);
      },
      error: (error) => {
        console.error(error);
        this.toaster.error('Usuário não carregado', 'Erro');
        this.router.navigate(['/dashboard']);
      },
    });
  }
  public validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmPassword'),
    };
    this.form = this.fb.group(
      {
        userName: [''],
        titulo: ['NaoInformado', Validators.required],
        primeiroNome: ['', Validators.required],
        ultimoNome: ['', Validators.required],
        email: [
          '',
          [
            Validators.required,
            Validators.email,
            Validators.minLength(4),
            Validators.maxLength(68),
          ],
        ],
        phoneNumber: ['', Validators.required],
        funcao: ['NaoInformadoe', Validators.required],
        descricao: [
          '',
          [
            Validators.required,
            Validators.minLength(4),
            Validators.maxLength(256),
          ],
        ],
        password: ['', [Validators.required, Validators.minLength(4)]],
        confirmPassword: ['', Validators.required],
        imagemUrl: [''],
      },
      formOptions
    );
  }

  onSubmit(): void {
    this.atualizarUsuario();
  }
  public atualizarUsuario() {
    this.userUpdate = { ...this.form.value };
    this.spinner.show();
    if (this.f.funcao.value == 'Palestrante') {
      this.palestranteService.addPalestrante().subscribe({
        next: () => {
          this.toaster.success('Função palestrante ativada!', 'Sucesso!');
        },
        error: (error) => {
          console.error(error);
          this.toaster.error('Erro ao ativar palestante', 'Error!');
        },
      });
    }

    this.accountService
      .updateUser(this.userUpdate)
      .subscribe({
        next: () => {
          this.toaster.success('Usuario atualizado com sucesso', 'Sucesso!');
        },
        error: (error) => {
          console.error(error);
          this.toaster.error(error.error);
        },
      })
      .add(() => {
        this.spinner.hide();
      });
  }
  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }
}
