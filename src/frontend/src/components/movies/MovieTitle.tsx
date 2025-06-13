import React from "react";
import TagButton from "../common/TagButton.tsx";
import { Bookmark, X } from "lucide-react";

type MovieTitleProps = {
  title: string;
  isInWatchList: boolean;
  isInNotInterested: boolean;
  onToggleWatchlist: () => void;
  onToggleNotInterested: () => void;
};

const getTitleSize = (title: string) => {
  if (title.length > 45) return "text-2xl";
  if (title.length > 25) return "text-3xl";
  return "text-5xl";
};

const MovieTitle: React.FC<MovieTitleProps> = ({
                                                 title,
                                                 isInWatchList,
                                                 onToggleWatchlist,
                                                 isInNotInterested,
                                                 onToggleNotInterested,
                                               }) => {
  const titleSize = getTitleSize(title);

  return (
    <div className="space-y-4">
      <h1 className={`${titleSize} font-bold`}>{title}</h1>
      <div className="flex gap-4 flex-wrap">
        <TagButton
          onClick={onToggleWatchlist}
          className={
            isInWatchList
              ? "bg-yellow-100 text-yellow-600 hover:bg-yellow-200"
              : "bg-gray-100 text-gray-800"
          }
        >
          <span className="flex items-center gap-2">
            {isInWatchList ? "В закладках" : "Буду смотреть"}
            <Bookmark
              className={
                isInWatchList
                  ? "w-5 h-5 stroke-yellow-600 fill-yellow-600"
                  : "w-5 h-5 stroke-gray-600 fill-none"
              }
            />
          </span>
        </TagButton>

        <TagButton
          onClick={onToggleNotInterested}
          className={
            isInNotInterested
              ? "bg-red-100 text-red-600 hover:bg-red-200"
              : "bg-gray-100 text-gray-800"
          }
        >
          <span className="flex items-center gap-2">
            {isInNotInterested ? "Неинтересно" : "Неинтересно"}
            <X
              className={
                isInNotInterested
                  ? "w-5 h-5 stroke-red-600 fill-red-600"
                  : "w-5 h-5 stroke-gray-600 fill-none"
              }
            />
          </span>
        </TagButton>
      </div>
    </div>
  );
};

export default MovieTitle;
