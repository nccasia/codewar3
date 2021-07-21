export interface OrderInt {
    links: string[];
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