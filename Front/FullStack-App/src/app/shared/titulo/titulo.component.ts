import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo = '';
  @Input() subtitulo ='Fullstack app';
  @Input() iconClass='fa fa-user';
  @Input() botaoListar = false;
  constructor() { }

  ngOnInit(): void {
  }

}