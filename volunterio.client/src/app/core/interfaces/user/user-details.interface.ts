import IUserAddress from './user-address.interface';


export interface IUserDetails {
    imageUrl?: string;
    imageThumbnailUrl?: string;
    address: IUserAddress;
}

export default IUserDetails;
