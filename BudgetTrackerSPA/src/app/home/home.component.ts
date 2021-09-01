import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../shared/models/userInfo';
import { UserService } from '../core/services/user.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  users!: UserInfo[];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    // ngOnInit is the right event to call our API
    this.userService.listAllUsers().subscribe(
      u => {
        this.users = u;
        console.log('inside home component init method');
        //console.table(this.users);
      }
    );
  }

}
