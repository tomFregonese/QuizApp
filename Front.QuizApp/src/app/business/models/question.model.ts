export interface Question {
    id: string;
    quizId: string;
    questionContent: string;
    options: string[];
    correctOptionIndices: number[];
}
