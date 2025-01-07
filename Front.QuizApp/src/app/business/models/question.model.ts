export interface Question {
    id: string;
    questionContent: string;
    options: string[];
    correctOptionIndices: number[];
}
