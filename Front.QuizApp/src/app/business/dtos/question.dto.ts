export interface QuestionDto {
    readonly id: string;
    readonly quizId: string;
    readonly questionContent: string;
    readonly options: string[];
}
