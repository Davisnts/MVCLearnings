import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Palestrante } from '@app/models/Palestrante';
import { PalestranteService } from '@app/services/palestrante.service';
import { NgxSpinnerModule, NgxSpinnerService } from 'ngx-spinner';
import { Toast, ToastrService } from 'ngx-toastr';
import { debounceTime, map, tap } from 'rxjs';

@Component({
  selector: 'app-palestrante-detalhe',
  templateUrl: './palestrante-detalhe.component.html',
  styleUrls: ['./palestrante-detalhe.component.css'],
})
export class PalestranteDetalheComponent implements OnInit {
  public form!: FormGroup;
  public situacaoDoForm = '';
  public corDaDescricao = '';
  constructor(
    private fb: FormBuilder,
    public palestranteService: PalestranteService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {
    this.validation();
    this.verificaForm(); 
    this.carregarPalestrante();
  }
  private validation(): void {
    this.form = this.fb.group({
      miniCurriculo: [''],
    });
  }
 
  private carregarPalestrante(): void{
    this.spinner.show();

    this.palestranteService.getPalestrante().subscribe({
      next:(palestrante:Palestrante)=>{this.form.patchValue(palestrante)},
      error:(error)=>{console.error(error),this.toastr.error("Erro ao carregar palestrante","Erro!")},
    })
  }
  public get f(): any {
    return this.form.controls;
  }
  private verificaForm(): void {
    this.form.valueChanges
      .pipe(
        map(() => {
          this.situacaoDoForm = 'Minicurrículo está sendo Atualizado';
          console.log(this.form.value)
          this.corDaDescricao = 'text-warning';
        }),
        debounceTime(1000),
        tap(()=> this.spinner.show())
      )
      .subscribe({
        next: () => {
          this.palestranteService
            .updatePalestrante({ ...this.form.value })
            .subscribe({
              next: () => {
                this.situacaoDoForm = 'Minicurrículo foi atualizado';
                this.corDaDescricao = 'text-success';

                setTimeout(()=>{ this.situacaoDoForm = 'Minicurrículo foi carregado';
                this.corDaDescricao = 'text-muted';}, 2000)

              },
              error:(error) => {this.toastr.error('Ocorreu um erro ao autualizar o Mini currriculo','error!')}
            }).add(()=> this.spinner.hide());
        },
      });
  }
}
