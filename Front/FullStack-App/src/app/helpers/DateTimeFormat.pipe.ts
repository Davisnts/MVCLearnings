import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';
import { Constants } from '../util/constants';

@Pipe({
  name: 'DateFormatPipe',
})
export class DateTimeFormatPipe extends DatePipe implements PipeTransform {
  override transform(value: any, args?: any): any {
    // Verifica se o valor é uma data válida antes de aplicar o DatePipe
    if (value instanceof Date && !isNaN(value.getTime())) {
      return super.transform(value, Constants.DATE_TIME_FMT);
    } else {
      return value; // Retorna o valor original se não for uma data válida
    }
  }
}

