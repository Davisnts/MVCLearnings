import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RedeSocial } from '@app/models/RedeSocial';
import { environment } from '@environments/environment';
import { Observable, take } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class RedeSocialService {
  baseUrl = environment.apiURL + 'RedeSociais';
  constructor(private http: HttpClient) {}

  
  public getRedesSociais(origem: string, id: number): Observable<RedeSocial[]> {
    let Url =
    id == 0
    ? `${this.baseUrl}/${origem}`
    : `${this.baseUrl}/${origem}/${id}`;
    return this.http.get<RedeSocial[]>(Url).pipe(take(1));
  }
  public salvarRedesSocais(
    origem: string,
    id: number,
    redesSociais: RedeSocial[]
  ): Observable<RedeSocial[]> {
    let Url =
      id == 0
        ? `${this.baseUrl}/${origem}`
        : `${this.baseUrl}/${origem}/${id}`;
    return this.http.put<RedeSocial[]>(Url,redesSociais).pipe(take(1));
  }
  public deleteRedeSocial(
    origem: string,
    id: number,
    redeSocialId: number
  ): Observable<any> {
    let Url =
      id == 0
        ? `${this.baseUrl}/${origem}/${redeSocialId}`
        : `${this.baseUrl}/${origem}/${id}/${redeSocialId}`;
    return this.http.delete<any>(Url).pipe(take(1));
  }
}
