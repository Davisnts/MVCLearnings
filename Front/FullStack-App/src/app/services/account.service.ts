import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '@app/models/Identity/User';
import { environment } from '@environments/environment';
import { Observable, ReplaySubject, map, take } from 'rxjs';

@Injectable()
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSource.asObservable();
  baseUrl = environment.apiURL + 'account/';
  constructor(private http: HttpClient) {}

  public login(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  logout(): void {
    localStorage.removeItem('user');
    this.currentUserSource.next(null as any);
    this.currentUserSource.complete;
  }
  public register(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }
  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }
}