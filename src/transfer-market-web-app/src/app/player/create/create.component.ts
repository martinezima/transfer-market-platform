import { Component, OnInit } from '@angular/core';
import { NationalityService } from '../services/nationality.service';
import { INationality } from '../models/nationality';
import { PlayerService } from '../services/player.service';
import { Router } from '@angular/router';
import { IPlayerDto } from '../models/player-create';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css'],
})
export class CreateComponent implements OnInit {
  nationalities: INationality[] = [];
  selectedNationalityId = '0';
  player: IPlayerDto = {
    id: crypto.randomUUID(),
    name: '',
    nationality: 0,
    age: 0,
    currentClub: '',
    transferCost: 0,
  };
  errorsResponse: any;

  constructor(
    private nationalityService: NationalityService,
    private playerService: PlayerService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadNationalities();
  }

  loadNationalities() {
    this.nationalityService.getAllNationalities().subscribe({
      next: data => {
        this.nationalities = data;
      },
      error: error => {
        console.error('Error loading nationalities:', error);
      },
    });
  }

  // Update nationality name when dropdown changes
  onNationalityChange(): void {
    const selected = this.nationalities.find(
      n => n.id === Number(this.selectedNationalityId)
    );
    this.player.nationality = selected?.id;
  }

  onSubmit(): void {
    this.errorsResponse = undefined;
    // Ensure nationality is set before submitting
    this.onNationalityChange();

    this.playerService.create(this.player).subscribe({
      next: (data: any) => {
        this.router.navigate(['/player/home']);
      },
      error: (errors: any) => {
        this.errorsResponse = errors;
      },
    });
  }
}
