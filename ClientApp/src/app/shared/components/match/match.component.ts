import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Match } from '../../interfaces';

@Component({
  selector: 'match',
  templateUrl: './match.component.html'
})
export class MatchComponent implements OnInit {

  @Input() match: Match;
  @Output() updateSubscription = new EventEmitter();
  subscribed: boolean;
  chatMessage: string = '';


  constructor() { }

  ngOnInit() {
  }

  setSubscription(val: boolean) {
    this.subscribed = val;
    let subscription =
        {
            subscribe: val,
            matchId: this.match.id
        }

    this.updateSubscription.emit(subscription);
  }

}
