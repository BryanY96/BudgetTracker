import { Component, Input, OnInit } from '@angular/core';
import { UserInfo } from '../../models/userInfo';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {
  @Input() user!: UserInfo;

  constructor() { }

  ngOnInit(): void {
  }

}
