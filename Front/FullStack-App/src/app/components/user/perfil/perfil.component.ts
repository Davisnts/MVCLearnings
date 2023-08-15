import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  form!: FormGroup;
  
  get f(): any {
    return this.form.controls;
  }  
  constructor(private fb: FormBuilder) {}
    ngOnInit(): void {
    this.validation();
  }
  public validation(): void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password','confirmPassword')
    
    }
    this.form = this.fb.group({
      titulo: ['0', Validators.required],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.minLength(4), Validators.maxLength(68)]],
      telefone: ['', Validators.required],
      funcao: ['Participante', Validators.required],
      descricao: ['',  [Validators.required, Validators.minLength(4), Validators.maxLength(256)]],
      password: ['', [Validators.required,Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    },formOptions);
  }
}



