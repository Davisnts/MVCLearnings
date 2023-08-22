import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent {
  form!: FormGroup;
  evento = {} as Evento;

  get f(): any {
    return this.form.controls;
  }
  stateSave = 'addEvento';
  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY HH:mm a',
      showWeekNumber: false,
    };
  }
  minDate: Date;

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private router: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private eventoService: EventoService
  ) {
    this.minDate = new Date();
    this.minDate.setDate(this.minDate.getDate());
    this.localeService.use('pt-br');
  }
  public carregarEvento(): void {
    const eventoIdParam = this.router.snapshot.paramMap.get('id');
    if (eventoIdParam !== null) {
      this.stateSave = 'updateEvento';
      this.eventoService.getEventoById(+eventoIdParam).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          this.form.patchValue(this.evento);
        },
        error: (error: any) => {
          console.error(error);
        },
        complete: () => {},
      });
    }
  }
  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
    console.log(this.form);
    console.log(this.stateSave);
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
      imagemUrl: ['', Validators.required],
    });
  }
  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campoForm: any): any {
    return { 'is-invalid': campoForm.errors && campoForm.touched };
  }
  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.evento =
        this.stateSave === 'addEvento'
          ? (this.evento = { ...this.form.value })
          : { id: this.evento.id, ...this.form.value };
    }
    this.eventoService[this.stateSave](this.evento).subscribe({
      next: () => this.toastr.success('Evento salvo com sucesso', 'Sucesso!'),
      error: () => {
        console.error(Error);
        this.toastr.error('Error ao salvar evento', 'Error');
      }
    }).add(() => this.spinner.hide());
  }
}
