import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserInfo } from 'src/app/shared/models/userInfo';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/app/shared/models/userDetails';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  // should have all the methods that deals with users: getById, addUser, updateUser, deleteUser, listAllUsers

  constructor(private http: HttpClient) { } // single-line dependency injection

  listAllUsers(): Observable<UserInfo[]> {
    // https://localhost:44363/api/User/allusers
    // model based on the JSON data returned by this URL

    return this.http.get(`${environment.apiUrl}` + 'User/allusers')
      .pipe(
        map(resp => resp as UserInfo[])
      )
  }

  getUserDetails(userId: number): Observable<User>{
    return this.http.get(`${environment.apiUrl}` + 'user/' + userId.toString())
    .pipe(
      map(resp => resp as User)
    )
  }

}
