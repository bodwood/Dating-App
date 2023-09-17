import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  // Allows us to pass data from the parent component to the child component.
  @Input() usersFromHomeComponent: any;
  // Allows us to emit an event from the child component to the parent component.
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: (error) => {
        console.log(error);
      },
    });
  }

  // Emits an event to the parent component (home.component.html).
  cancel() {
    this.cancelRegister.emit(false);
  }
}
