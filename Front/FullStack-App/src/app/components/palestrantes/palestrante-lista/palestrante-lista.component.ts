import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Palestrante } from '@app/models/Palestrante';
import { PaginatedResult, Pagination } from '@app/models/Pagination';
import { PalestranteService } from '@app/services/palestrante.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Subject, debounceTime } from 'rxjs';
import { environment } from '@environments/environment';


@Component({
  selector: 'app-palestrante-lista',
  templateUrl: './palestrante-lista.component.html',
  styleUrls: ['./palestrante-lista.component.css']
})
export class PalestranteListaComponent implements OnInit {
  public palestrantes: Palestrante[] = [];
  public eventoId = 0;
  public pagination = {} as Pagination;
  constructor(
    private palestranteService: PalestranteService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 1,
    } as Pagination;
   this.carregarPalestrantes();
     }

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarPalestrante(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged
        .pipe(debounceTime(1000))
        .subscribe((filtrarPor) => {
          this.spinner.show();
          this.palestranteService
            .getPalestrantes(
              this.pagination.currentPage,
              this.pagination.itemsPerPage,
              filtrarPor
            )
            .subscribe({
              next: (paginatedResult: PaginatedResult<Palestrante[]>) => {
                this.palestrantes = paginatedResult.result;
                this.pagination = paginatedResult.pagination;
                console.log(this.palestrantes);
              },
              error: (error) => {
                this.spinner.hide();
                this.toastr.error('Erro ao Carregar os Palestrante', 'Erro!');
                console.error(error);
              },
            })
            .add(() => this.spinner.hide());
        });
    }
    this.termoBuscaChanged.next(evt.value);
  }
  public getImagemUrl(imagemName:string): string{
    if (imagemName)  
    return `${environment.apiURL}resources/Perfil/${imagemName}`;
    else
    return 'assets/img/perfil.png'
  }
  public carregarPalestrantes():void{
    this.spinner.show();
    this.palestranteService
      .getPalestrantes(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe({
        next: (paginatedResult: PaginatedResult<Palestrante[]>) => {
          this.palestrantes = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        error: (error) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar os Palestrantes', 'Erro!');
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }

}
