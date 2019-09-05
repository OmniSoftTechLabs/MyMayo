import { Component, Inject } from '@angular/core';
import { userMaster } from '../models/userModel';
import { UserRoleService } from '../Services/user-role.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  firstName: string;
  userName: string;
  qualification: string ="0";
  userModel: userMaster = new userMaster();
  userList: userMaster[];

  constructor(private userService: UserRoleService) {
  }

  ngOnInit() {
    return this.userService.getAllUser().subscribe(result => {
      this.userList = result;
    }, error => console.error(error));
  }

  onSubmit() {

    this.userModel.firstName = this.firstName;
    this.userModel.userName = this.userName;
    this.userModel.qualification = this.qualification;

    return this.userService.addTeacherUser(this.userModel).subscribe(result => {
      this.userModel = result;
    }, error => console.error(error));
  }
}

