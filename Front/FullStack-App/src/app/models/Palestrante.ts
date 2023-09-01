import { Evento } from './Evento';
import { UserUpdate } from './Identity/UserUpdate';
import { RedeSocial } from './RedeSocial';

export interface Palestrante {
  id: number;
  nome: string;
  miniCurriculo: string;
  user: UserUpdate;
  redesSociais: RedeSocial[];
  palestrantesEventos: Evento[];
}
