interface ICreateHelpRequestRequest {
    title: string;
    description: string;
    tags: string[];
    latitude?: number;
    longitude?: number;
    showContactInfo: boolean;
    deadline?: Date;
    images: string[];
}

export default ICreateHelpRequestRequest;
