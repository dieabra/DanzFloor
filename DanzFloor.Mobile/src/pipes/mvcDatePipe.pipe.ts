import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
@Pipe({
	name: 'mvcDatePipe'
})
export class MvcDatePipe implements PipeTransform {
    constructor(private datePipe: DatePipe) {}
	transform(value: string, length: number): string {
		if (!value) {
			return value;
		}

		return this.datePipe.transform(new Date(parseInt(value.replace("/Date(", "").replace(")/",""), 10)), 'yyyy-MM-dd');// .toString();
	}
}