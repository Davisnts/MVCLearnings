import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable, take } from 'rxjs';

@Injectable()
export class LoteService {


  baseURL = 'https://api.onlycode.com.br:5001/Lotes';

  constructor(private http: HttpClient) { }


  public getLoteByEventoId(id: number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseURL}/${id}`).pipe(take(1));
  }
  
  public updateLote(eventoId:number , lotes : Lote): Observable<Lote> {
    return this.http.put<Lote>(`${this.baseURL}/${eventoId}/`, lotes).pipe(take(1));
  }
  public deleteLote(eventoId: number, loteId:number):Observable<any> {
    return this.http.delete(`${this.baseURL}/${eventoId}/${loteId}`).pipe(take(1));

  
}
}