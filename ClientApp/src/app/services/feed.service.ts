import { HubConnection } from '@aspnet/signalr';
import { Injectable } from '@angular/core';
import { SignalRConnectionStatus, Match, Feed, ChatMessage, FeedServer, FeedSignalR } from '../shared/interfaces';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { Http } from '@angular/http';
import { HubConnectionBuilder } from '@aspnet/signalr';

@Injectable()
export class FeedService {

  currentState = SignalRConnectionStatus.Disconnected;
  connectionState: Observable<SignalRConnectionStatus>;

  setConnectionId: Observable<string>;
  updateMatch: Observable<Match>;
  addFeed: Observable<Feed>;
  addChatMessage: Observable<ChatMessage>;

  private connectionStateSubject = new Subject<SignalRConnectionStatus>();
    
  private setConnectionIdSubject = new Subject<string>();
  private updateMatchSubject = new Subject<Match>();
  private addFeedSubject = new Subject<Feed>();
  private addChatMessageSubject = new Subject<ChatMessage>();

  private server: FeedServer;
  private sv : HubConnection;

  constructor(private http: Http) { 
    this.connectionState = this.connectionStateSubject.asObservable();

    this.setConnectionId = this.setConnectionIdSubject.asObservable();
    this.updateMatch = this.updateMatchSubject.asObservable();
    this.addFeed = this.addFeedSubject.asObservable();
    this.addChatMessage = this.addChatMessageSubject.asObservable();
  }

  init() {
    let connection = new HubConnectionBuilder().withUrl("/broadcaster").build();
    this.sv = connection;

    // setConnectionId method called by server
    connection.on("setConnectionId",id => this.onSetConnectionId(id))
    // updateMatch method called by server
    connection.on("updateMatch",match => this.onUpdateMatch(match));

    // start the connection
    connection.start()
    .then(response => this.setConnectionState(SignalRConnectionStatus.Connected))
    .catch(err => console.error(err.toString()));
  }

  private setConnectionState(connectionState: SignalRConnectionStatus) {
    console.log('connection state changed to: ' + connectionState);
    this.currentState = connectionState;
    this.connectionStateSubject.next(connectionState);
  }


  // Client side methods
  private onSetConnectionId(id: string) {
    console.log('onSetConnectionId: ===> '+id);
    this.setConnectionIdSubject.next(id);
  }

  private onUpdateMatch(match: Match) {
    console.log('onUpdateMatch');
    console.log(match);
    this.updateMatchSubject.next(match);
  }

  // Server side methods
  public subscribeToFeed(matchId: number) {
    this.sv.invoke("subscribe",matchId);
  }

  public unsubscribeFromFeed(matchId: number) {
      this.sv.invoke("unsubscribe",matchId);
  }

}
