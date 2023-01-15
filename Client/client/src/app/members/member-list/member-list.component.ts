import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { debounceTime, distinctUntilChanged, filter, fromEvent, tap } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  @ViewChild('membersSearch', {static: true}) input: ElementRef;
  members: Member[];
  friends: Member[];
  public message = '';

  constructor(private memberService: MembersService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadMembers();
    this.loadFriends();
  }

  loadMembers() {
    this.memberService.getMembers().subscribe(members => {
        this.members = members;
    })
  }

  findMembers(text: string) {
    this.memberService.findMembers(text).subscribe(members => {
      this.members = members;
  })
  }

  loadFriends() {
    this.memberService.getFriends().subscribe(members => {
        this.friends = members;
    })
  }

  addToFriendList(event: number)
  {
    const friend = this.members.find((member) => {return member.id == event});
    const friendIndex = this.members.findIndex((member) => {return member.id == event});
    this.friends.push(friend);
    this.members.splice(friendIndex, 1);
  }

  removeFromFriendList(event: number)
  {
    const member = this.friends.find((friend) => {return friend.id == event});
    const memberIndex = this.friends.findIndex((friend) => {return friend.id == event});
    this.members.push(member);
    this.friends.splice(memberIndex, 1);
  }

  ngAfterViewInit() {
    fromEvent<KeyboardEvent>(this.input.nativeElement,'keyup')
      .pipe(
        filter(Boolean),
        debounceTime(500),
        distinctUntilChanged(),
        tap(() => {
          const value = this.input.nativeElement.value;
          if (value.length > 2) {
            this.memberService.findMembers(value).subscribe((members) => {
              if(members.length != 0)
              {
                this.message = '';
                this.members = members;
              }
              else {
                this.message = 'Brak użytkowników o tej nazwie';
                this.loadMembers();
              }

            });
          } else {
            this.loadMembers();
            if(value.length == 0){
              this.message = '';
            }
            else{
              this.message = 'Proszę wprowadź minimum 3 znaki, aby znaleźć użytkownika';
            }
            
            
          }
        })
      ).subscribe();
  }
}
