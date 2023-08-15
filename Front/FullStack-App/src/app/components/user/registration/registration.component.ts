import { Component } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent {
  
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
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email, Validators.minLength(4), Validators.maxLength(68)]],
      userName: ['', Validators.required],
      password: ['', [Validators.required,Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
    },formOptions);
  }
}
