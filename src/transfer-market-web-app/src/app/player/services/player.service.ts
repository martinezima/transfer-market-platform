import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPlayer } from '../models/player';
import { IPlayerDto } from '../models/player-create';

@Injectable({
  providedIn: 'root',
})
export class PlayerService {
  constructor(private http: HttpClient) {}

  getAllPlayers() {
    return this.http.get<IPlayer[]>('/api/players');
  }

  create(playerToCreate: IPlayerDto) {
    return this.http.post('/api/players', playerToCreate);
  }

  getPlayerById(id: string | null) {
    return this.http.get<IPlayerDto>(`/api/players/${id}`);
  }

  update(playerToUpdate: IPlayerDto) {
    return this.http.put(`/api/players/${playerToUpdate.id}`, playerToUpdate);
  }

  delete(id: string) {
    return this.http.delete<boolean>(`/api/players/${id}`);
  }
}
