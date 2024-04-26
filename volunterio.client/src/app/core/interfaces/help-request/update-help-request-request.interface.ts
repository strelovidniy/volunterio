interface IUpdateHelpRequestRequest {
    id: string;
    title: string;
    description: string;
    tags: string[];
    latitude?: number;
    longitude?: number;
    showContactInfo: boolean;
    deadline?: Date;
    images: string[];
    imagesToDelete: string[];
}

export default IUpdateHelpRequestRequest;
