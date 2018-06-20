import { DataService } from './../services/data.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Match, SignalRConnectionStatus, Feed } from '../shared/interfaces';
import { FeedService } from '../services/feed.service';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/takeUntil';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.css']
})
export class ScoresComponent implements OnInit,OnDestroy {

/* A subject for monitoring the destruction of the component. */
  private destroyed$: Subject<{}> = new Subject();

  matches: Match[];
  connectionId: string;
  error: any;

  constructor(
    private feedService: FeedService,
    private dataService: DataService
  ) { }

  ngOnInit() {
    let self = this;
    self.listenForConnection();
    self.loadMatches();

    if(self.feedService.currentState == SignalRConnectionStatus.Connected) {
        self.listenForMatchUpdate();
    }
    else {
        self.feedService.connectionState.subscribe(
            connectionState => {
                if (connectionState == SignalRConnectionStatus.Connected) {
                    console.log('Connected!');
      
                 self.listenForMatchUpdate();
                    
                } else {
                    console.log(connectionState.toString());
                }
            },
            error => {
                this.error = error;
                console.log(error);
            });
    }
  }

  ngOnDestroy() {
    this.destroyed$.next(); /* Emit a notification on the subject. */
  }

  listenForConnection() {
    let self = this;
    // Listen for connected / disconnected events
    self.feedService.setConnectionId.subscribe(
        id => {
            console.log(id);
            self.connectionId = id;
        }
    );
  }

  listenForMatchUpdate() {
    let self = this;
    // Listen for match score updates...
    self.feedService.updateMatch
    .takeUntil( this.destroyed$ ) /* Unsubscribe from the stream upon destruction. */
    .subscribe(
        match => {
            console.log('self.feedService.updateMatch.subscribe');
            console.log(match);
            for (var i = 0; i < self.matches.length; i++) {
                if (self.matches[i].id === match.id) {
                    self.matches[i].hostScore = match.hostScore;
                    self.matches[i].guestScore = match.guestScore;

                    if(match.hostScore === 0 && match.guestScore === 0)
                        self.matches[i].feeds = new Array<Feed>();
                }
            }
        }
    );
  }

  loadMatches(): void {
    let self = this;
    this.dataService.getMatches()
        .subscribe((res: Match[]) => {
            self.matches = res;
        },
        error => {
            console.log(error);
        });
    }

}
