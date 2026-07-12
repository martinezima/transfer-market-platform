import { Injectable } from '@angular/core';
import { INationality } from '../models/nationality';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class NationalityService {
  constructor(private http: HttpClient) {}

  getAllNationalities() {
    return this.http.get<INationality[]>('/api/nationality');
  }
}
