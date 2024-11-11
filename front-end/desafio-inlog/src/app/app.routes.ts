import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VehicleRegisterPageComponent } from './pages/vehicle-register-page/vehicle-register-page.component';
import { VehicleListPageComponent } from './pages/vehicle-list-page/vehicle-list-page.component';

export const routes: Routes = [
  { path: '', redirectTo: '/listar', pathMatch: 'full' },
  { path: 'listar', component:  VehicleListPageComponent},
  { path: 'cadastrar', component: VehicleRegisterPageComponent },
  { path: '**', redirectTo: '/listar' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

