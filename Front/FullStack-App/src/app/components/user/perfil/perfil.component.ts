import { Component, OnInit } from '@angular/core';
import { UserUpdate } from '@app/models/Identity/UserUpdate';
import { AccountService } from '@app/services/account.service';
import { environment } from '@environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  // eslint-disable-next-line @angular-eslint/component-selector
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss'],
})
export class PerfilComponent implements OnInit {
  public user = {} as UserUpdate;
  public file: File;
  public imagemUrl = '';
  public get ehPalestrante(): boolean {
    return this.user.funcao == 'Palestrante';
  }

  constructor(
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private accountService : AccountService
  ) {}

  ngOnInit(): void {}
  public setFormValue(user: UserUpdate): void {
    this.user = user;
    if (this.user.imagemUrl)  
    this.imagemUrl = `${environment.apiURL}resources/Perfil/${this.user.imagemUrl}`;
    else
    this.imagemUrl = 'assets/img/perfil.png'
  console.log(user.imagemUrl)
  }
    private uploadImagem(): void {
    this.spinner.show;
    this.accountService
      .postUpload(this.file)
      .subscribe({
        next: () => {
          this.toastr.success('Imagem autoalizado com sucesso', 'Sucesso!');
        },
        error: (error) => {
          this.toastr.error('Erro ao atualizar imagem', 'Error!');
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }
  onFileChange(ev: any): void {
    const reader = new FileReader();
    reader.onload = (event: any) => (this.imagemUrl = event.target.result);
    this.file = ev.target.files;
    reader.readAsDataURL(this.file[0]);
    this.uploadImagem();
  }
}
