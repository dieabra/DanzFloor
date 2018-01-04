import { data } from './home-data';
import { describe, it, expect } from 'jasmine-core';

describe('Home data', () => {
	it('should contain valid data for the home page', () => {
		expect(data.facebook).toBe('https://www.facebook.com/ionicframework');
	});
});