import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { User } from '../models/users';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = 'http://localhost:5145/api/user';
  private usersList = new BehaviorSubject<User[]>([]);
  usersList$ = this.usersList.asObservable();

  constructor(private http: HttpClient) { }

  loadUsersList() {
    this.http.get<User[]>(this.baseUrl).subscribe((response) => {
      this.usersList.next(response);
    });
  }

  deleteUserById(userId: number) {
    this.http.delete(this.baseUrl + '/' + userId).subscribe(() => {
      this.loadUsersList();
    });
  }

  updateUser(user: User) {
    this.http.put(this.baseUrl, user).subscribe(() => {
      this.loadUsersList();
    });
  }

  addUser(user: User) {
    const newUser = {
      name: user.name,
      email: user.email
    }
    this.http.post(this.baseUrl, newUser).subscribe(() => {
      this.loadUsersList();
    });
  }

  getUserBy(userId: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + '/' + userId);
  }
}
