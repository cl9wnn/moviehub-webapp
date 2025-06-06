import React from "react";

type Props = {
  searchQuery: string;
  setSearchQuery: (val: string) => void;
  searchType: "movies" | "actors";
  setSearchType: (val: "movies" | "actors") => void;
};

export const SearchInput: React.FC<Props> = ({
                                               searchQuery,
                                               setSearchQuery,
                                               searchType,
                                               setSearchType,
                                             }) => (
  <div className="flex items-center">
    <input
      type="text"
      placeholder="Поиск..."
      value={searchQuery}
      onChange={(e) => setSearchQuery(e.target.value)}
      className="flex-grow px-4 py-2 rounded-l-xl bg-[#474747] text-white placeholder:text-[#cccccc] focus:outline-none h-[38px]"
    />
    <select
      value={searchType}
      onChange={(e) => setSearchType(e.target.value as "movies" | "actors")}
      className="bg-[#474747] text-white px-3 h-[38px] rounded-r-xl border-l-4 border-[#111111] text-l leading-[38px]"
    >
      <option value="movies">Фильмы</option>
      <option value="actors">Актёры</option>
    </select>
  </div>
);
