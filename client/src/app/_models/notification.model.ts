export interface ApiNotification{
    id: number;
    insertDate: Date;
    content: string;
    recipientId: number;
    read?: boolean;
    deleted?: boolean;
    notificationTypeId:number;
    notificationType : NotificationType
}

export interface NotificationType{
    id:number;
    name:string;
}