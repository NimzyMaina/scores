import { Component, OnInit, Input } from '@angular/core';
import { Match, ChatMessage } from '../../interfaces';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html'
})
export class ChatComponent implements OnInit {

  @Input() matches: Match[];
  @Input() connection: string;
  messages: ChatMessage[];

  constructor() { }

  ngOnInit() {
  }

}
