import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { ListarEventos } from '../../../models/evento/ListarEventos';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent {

  admin:boolean = true; // Definindo a variável admin como false
  eventoList: any[] = []; // Definindo o tipo de eventoList como array de objetos
  stateSave = 'post'; // Definindo a variável stateSave como post

  constructor(private eventoService: EventoService, private router : Router) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos() {
    this.eventoService.get().subscribe((data: ListarEventos) => {
      console.log(data);
      this.eventoList = data.data;
    });
  }

  public editarEvento(id: number): void {
    this.router.navigate([`eventos/editar/${id}`]);
  }

  public novoEvento(): void {
    this.router.navigate(['eventos/novo']);
  }

  // loadEvento(): void {
  //   const personIdParam = this.route.snapshot.paramMap.get('id');

  //   if (personIdParam !== null) {
  //     this.stateSave = 'put';

  //     this.listServices.getPersonById(personIdParam).subscribe(
  //       (person: Person) => {
  //         this.person = { ...person };
  //         this.person.addressId = this.person.address.id;
  //         this.person.companyId = this.person.company.id;
  //         this.form.patchValue(this.person);
  //       },
  //       (error) => console.log(error)
  //     );
  //   }
  // }

}
