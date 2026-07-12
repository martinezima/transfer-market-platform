import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './player/home/home.component';
import { CreateComponent } from './player/create/create.component';
import { EditComponent } from './player/edit/edit.component';

const routes: Routes = [
  { path: 'player/home', component: HomeComponent },
  { path: 'player', redirectTo: 'player/home', pathMatch: 'full' },
  { path: '', redirectTo: 'player/home', pathMatch: 'full' },
  { path: 'player/create', component: CreateComponent },
  { path: 'player/edit/:id', component: EditComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
