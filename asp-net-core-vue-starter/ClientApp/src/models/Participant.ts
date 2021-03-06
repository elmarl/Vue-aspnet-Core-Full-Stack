﻿import { Event } from './Event';
export class Participant {
    constructor(
        public participantid: number,
        public firstname: string,
        public familyname: string = '',
        public idcode: string,
        public numParticipants: number = 0,
        public paymentmethod: string,
        public detailsPerson: string,
        public detailsCompany: string,
        public participantType: string,
        public event: Event,
    ) { }
}
