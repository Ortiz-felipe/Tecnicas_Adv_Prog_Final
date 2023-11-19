import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
} from "@mui/material";
import styled from "@emotion/styled";

const StyledTableContainer = styled(TableContainer)(({ theme }) => ({
  margin: theme.spacing(3, 0),
  boxShadow: "none",
}));

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  borderBottom: "none",
  padding: theme.spacing(2),
  "&:last-child": {
    paddingRight: theme.spacing(2),
  },
}));

const StyledTableHeadCell = styled(StyledTableCell)(({ theme }) => ({
  color: theme.palette.text.secondary,
  fontWeight: "normal",
}));

interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: "right" | "left" | "center";
  format?: (value: any) => string;
}

interface Action<T> {
  icon: React.ReactElement;
  tooltip?: string;
  onClick: (item: T) => void;
}

interface TableComponentProps<T> {
  columns: Column[];
  data: T[];
  actions?: Action<T>[];
  renderFavoriteIcon?: (item: T) => React.ReactNode;
}

function TableComponent<T>({
  columns,
  data,
  actions,
  renderFavoriteIcon,
}: TableComponentProps<T>): JSX.Element {
  return (
    <StyledTableContainer component={Paper}>
      <Table stickyHeader aria-label="sticky table">
        <TableHead>
          <TableRow>
            {renderFavoriteIcon && <TableCell />}
            {columns.map((column) => (
              <StyledTableHeadCell
                key={column.id}
                align={column.align}
                style={{ minWidth: column.minWidth }}
              >
                {column.label}
              </StyledTableHeadCell>
            ))}
            {actions && actions.length > 0 && (
              <StyledTableHeadCell align="center">Acciones</StyledTableHeadCell>
            )}
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((item, index) => (
            <TableRow
              hover
              role="checkbox"
              tabIndex={-1}
              key={item.id || index}
            >
              {renderFavoriteIcon && (
                <TableCell>{renderFavoriteIcon(item)}</TableCell>
              )}
              {columns.map((column) => {
                const value = item[column.id];
                return (
                  <TableCell key={column.id} align={column.align}>
                    {column.format ? column.format(value) : value}
                  </TableCell>
                );
              })}
              {actions && (
                <TableCell align="center">
                  {actions.map((action, actionIndex) => (
                    <IconButton
                      key={actionIndex}
                      onClick={() => action.onClick(item)}
                      title={action.tooltip}
                    >
                      {action.icon}
                    </IconButton>
                  ))}
                </TableCell>
              )}
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </StyledTableContainer>
  );
}

export default TableComponent;
