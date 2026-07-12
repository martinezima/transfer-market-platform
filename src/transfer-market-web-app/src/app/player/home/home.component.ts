import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../services/player.service';
import { IPlayer } from '../models/player';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  players: IPlayer[] = [];
  constructor(private playerService: PlayerService) {}

  ngOnInit(): void {
    this.playerService.getAllPlayers().subscribe({
      next: (players: IPlayer[]) => {
        this.players = players;
      },
      error: error => {
        console.error('Error loading players: ', error);
      },
    });
  }

  deletePlayer(id: string) {
    this.playerService.delete(id).subscribe({
      next: (deleted: boolean) => {
        console.log(`Player with id ${id} was deleted`);
        this.players = this.players.filter(p => p.id != id);
      },
      error: error => {
        console.error('Error deleting players: ', error);
      },
    });
  }
}
