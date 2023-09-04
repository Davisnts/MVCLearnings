import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { Router } from '@angular/router';
import { environment } from '@environments/environment';
import { PaginatedResult, Pagination } from '@app/models/Pagination';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss'],
})
export class EventoListaComponent {
  modalRef?: BsModalRef;
  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public eventoId = 0;
  public larguraImagem = 150;
  public margemImagem = 2;
  public exibirImagem = true;
  public pagination = {} as Pagination;

  termoBuscaChanged: Subject<string> = new Subject<string>();

  public filtrarEventos(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged
        .pipe(debounceTime(1000))
        .subscribe((filtrarPor) => {
          this.spinner.show();
          this.eventoService
            .getEventos(
              this.pagination.currentPage,
              this.pagination.itemsPerPage,
              filtrarPor
            )
            .subscribe({
              next: (paginatedResult: PaginatedResult<Evento[]>) => {
                this.eventos = paginatedResult.result;
                this.pagination = paginatedResult.pagination;
              },
              error: (error) => {
                this.spinner.hide();
                this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
                console.error(error);
              },
            })
            .add(() => this.spinner.hide());
        });
    }
    this.termoBuscaChanged.next(evt.value);
  }

  constructor(
    private eventoService: EventoService,
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
    this.getEventos();
  }
  detalheEvento(Eventoid: number): void {
    this.router.navigate([`/eventos/detalhe/${Eventoid}`]);
  }

  public alterarImagem(): void {
    this.exibirImagem = !this.exibirImagem;
  }
  public mostraImagem(imagemUrl): string {
    return imagemUrl != ''
      ? `${environment.apiURL}resources/Eventos/${imagemUrl}`
      : `assets/img/semimagem.jpg`;
  }

  public getEventos(): void {
    this.spinner.show();
    this.eventoService
      .getEventos(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe({
        next: (paginatedResult: PaginatedResult<Evento[]>) => {
          this.eventos = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
        },
        error: (error) => {
          this.spinner.hide();
          this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(eventoId: any): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService
      .deleteEvento(this.eventoId)
      .subscribe({
        next: (result: any) => {
          if (result.message == 'Deletado') {
            console.log(result);
            this.toastr.success(
              `O Evento de ID ${eventoId} foi deletado com sucesso!`,
              'Deletado!'
            );
            this.getEventos();
          }
        },
        error: (error: any) => {
          this.toastr.error(
            `Ocorreu um erro ao deletar o evento ${eventoId}`,
            'Error!'
          );
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }
  pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.getEventos();
  }
}
