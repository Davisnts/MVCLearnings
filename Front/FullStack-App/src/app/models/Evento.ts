import { RedeSocial } from './RedeSocial';
import { Lote } from "./Lote";
import { Palestrante } from './Palestrante';

export interface Evento {
  id: number;
  local: string;
  dataEvento?: Date;
  tema: string;
  qtdPessoas: number;
  imagemUrl: string;
  telefone: string;
  email: string;
  userId: number;
  lotes: Lote[];
  redesSociais: RedeSocial[];
  palestrantesEventos: Palestrante[];
}
