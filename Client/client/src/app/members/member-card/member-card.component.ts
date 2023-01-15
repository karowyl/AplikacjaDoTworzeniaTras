import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | undefined;
  @Input() isFriendTab: boolean ;
  @Output() friendAddedEvent = new EventEmitter() ;
  @Output() friendRemovedEvent = new EventEmitter() ;
  
  constructor(private memberService: MembersService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  
  addFriend(member: Member) {
    this.memberService.addFriend(member.id).subscribe({
      next: () => {
        this.toastr.success('Dodałeś ' + member.username);
        this.friendAddedEvent.emit(member.id);
      } 
  })
  }

  removeFriend(member: Member) {
    this.memberService.removeFriend(member.id).subscribe({
      next: () => {
          this.toastr.success('Usunąłeś ' + member.username);
          this.friendRemovedEvent.emit(member.id);
      } 
  })
  }


}
