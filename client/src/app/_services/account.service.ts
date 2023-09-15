import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_models/user';

// Allows for services to be injected into components.
// Services are Singleton objects that get instantiated only once during the lifetime of an application.
// Singletons are useful for sharing data between various components of an application.
// Singletons are only destroyed when the application is closed.
@Injectable({
  providedIn: 'root',
})

// This service will be used to make requests to the API.
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';

  // BehaviorSubject is a special type of observable that allows us to set its initial value. We can then use that value to emit to other components.
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {}

  login(model: any) {
    // Posts the model to the API. There are three parameters: the base URL, the endpoint, and the model.
    // This returns an observable that we can subscribe to.
    // The map operator allows us to transform the response from the API into a User object.
    // Expects a response of type User.
    // Pipe allows us to chain multiple rxjs operators together.
    // Map transforms the response into a User object.
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      // Cast response to User.
      map((response: User) => {
        var user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          // Emit the user object to all subscribers.
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
