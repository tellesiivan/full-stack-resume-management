import {HttpStatusCode} from "axios";

export type BaseResponse = {
	isSuccess?: boolean;
	message?: string
}
export type Response<T> = {
	isSuccess?: boolean;
	data?: T;
	errorMessage?: string;
	statusCode?: HttpStatusCode;
}


export type CompanyResponseDto = {
	id?: number;
	createdAt?: Date;
	updatedAt?: Date;
	isActive?: boolean;
	name?: string;
	size?: string;
}

export type JobResponseDto = {
	id?: number;
	createdAt?: Date;
	title?: string;
	description?: string;
	jobLevel?: string;
	companyId?: number;
	companyName?: string;
}
export type CandidatesResponseDto = {
	id?: number;
	firstName?: string;
	lastName?: string;
	email?: string;
	phoneNumber?: string;
	coverLetter?: string;
	resumeUrl?: string;
	jobId?: number;
}