import { Component, OnDestroy, OnInit } from '@angular/core';
import { User } from '../models/users';
import { UserService } from '../services/user.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit, OnDestroy {


  constructor(
    private userService: UserService,
    private router: Router) { }

  users: User[] = [];
  editUser: User | null = null;

  private usersSubscription = new Subscription();

  ngOnInit(): void {

    this.usersSubscription = this.userService.usersList$.subscribe(users => {
      this.users = [];
      if (users.length) {
        this.users = users;
      }
    });
  }

  onDeleteClick(userId: number) {
    this.userService.deleteUserById(userId);
  }

  switchEdit(user: User | null) {
    this.editUser = user;

    if(!this.users[0].id) this.users.shift();
  }

  onSaveClick() {
    if (this.editUser) { 
      this.editUser.id !== 0 
      ? this.userService.updateUser(this.editUser)
      : this.userService.addUser(this.editUser);
      
      this.editUser = null;
    }
  }

  onNewClick() {
    let newUser: User = {
      id: 0,
      name: '',
      email: ''
    };

    this.users.unshift(newUser);
    this.editUser = this.users[0];
  }

  onDetailsClick(userId: number){
    this.router.navigate(['/user-details', { userId }]);
  }

  ngOnDestroy(): void {
    this.usersSubscription.unsubscribe();
  }
}
