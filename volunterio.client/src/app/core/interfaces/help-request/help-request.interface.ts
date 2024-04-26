import IContactDetails from '../user/contact-details.interface';
import IHelpRequestImage from './help-request-image.interface';


interface IHelpRequest {
    id: string;
    title: string;
    description: string;
    tags: string[];
    latitude?: number;
    longitude?: number;
    contactInfo: IContactDetails;
    deadline: Date;
    images: IHelpRequestImage[];
}

export default IHelpRequest;
