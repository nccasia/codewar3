export interface OrderInt {
    links:  OrderInfoInt[];
}

export interface OrderInfoInt {
    id: string;
    link: string;
}

export interface TrackListsInt {
    msgId: string;
    totalReact: number;
    title: string | undefined;
    userName: string;
    url: string | undefined;
    youtubeId: string | undefined;
    lastTimeOrder: number;
    isPlaying: boolean;
}