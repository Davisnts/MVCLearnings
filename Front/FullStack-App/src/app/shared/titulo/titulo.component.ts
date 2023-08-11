import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-Titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.css']
})
export class tituloComponent implements OnInit {
  constructor() { }
  public 'Titulo' =  Component['name'];
  
  ngOnInit() {
  }

}
