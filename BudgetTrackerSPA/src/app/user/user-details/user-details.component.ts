import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/core/services/user.service';
import { User } from 'src/app/shared/models/userDetails';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  users!: User;
  id!: number;

  constructor(private userService: UserService, private currentRouter: ActivatedRoute) { }

  ngOnInit(): void {
    this.currentRouter.params.subscribe(
      f => {
        this.id = f['id'];
      });
      console.log(this.id)
      this.userService.getUserDetails(this.id).subscribe(
        u => {
          this.users = u;
        }
      )
  }

}
