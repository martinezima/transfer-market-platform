import { Component, OnInit } from '@angular/core';
import { PlayerService } from '../services/player.service';
import { ActivatedRoute, Router } from '@angular/router';
import { IPlayerDto } from '../models/player-create';
import { INationality } from '../models/nationality';
import { NationalityService } from '../services/nationality.service';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css'],
})
export class EditComponent implements OnInit {
  nationalities: INationality[] = [];
  selectedNationalityId!: string | null;
  player: IPlayerDto = {
    id: '',
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
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(param => {
      let id = param.get('id');
      this.loadData(id);
    });
  }

  loadData(id: string | null) {
    // Load both in parallel
    forkJoin({
      player: this.playerService.getPlayerById(id),
      nationalities: this.nationalityService.getAllNationalities(),
    }).subscribe({
      next: result => {
        this.player = result.player;
        this.nationalities = result.nationalities;
        this.selectedNationalityId = this.player.nationality?.toString() ?? '0';
      },
      error: error => {
        console.error('Error loading data:', error);
        this.errorsResponse = error;
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

    this.playerService.update(this.player).subscribe({
      next: (data: any) => {
        this.router.navigate(['/player/home']);
      },
      error: (errors: any) => {
        this.errorsResponse = errors;
      },
    });
  }
}
