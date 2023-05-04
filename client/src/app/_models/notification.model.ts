export interface Notification{
    id: number;
    insertDate: Date;
    content: string;
    recipientId: number;
    read?: boolean;
    deleted?: boolean;
    notificationTypeId:number;
}