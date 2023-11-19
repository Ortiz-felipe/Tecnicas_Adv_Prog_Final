import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  IconButton,
} from "@mui/material";
import styled from "@emotion/styled";
import { Theme } from '@mui/material/styles';

const StyledTableContainer = styled(TableContainer)<{ theme?: Theme }>(({ theme }) => ({
  margin: theme.spacing(3, 0),
  boxShadow: "none",
}));

const StyledTableCell = styled(TableCell)<{ theme?: Theme }>(({ theme }) => ({
  borderBottom: "none",
  padding: theme.spacing(2),
  "&:last-child": {
    paddingRight: theme.spacing(2),
  },
}));

const StyledTableHeadCell = styled(StyledTableCell)<{ theme?: Theme }>(({ theme }) => ({
  color: theme.palette.text.secondary,
  fontWeight: "normal",
}));

interface TableItem {
  id?: string | number;
}

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

// Extend generic type T with TableItem
interface TableComponentProps<T extends TableItem> {
  columns: Column[];
  data: T[];
  actions?: Action<T>[];
  renderFavoriteIcon?: (item: T) => React.ReactNode;
}

function TableComponent<T extends TableItem>({
  columns,
  data,
  actions,
  renderFavoriteIcon,
}: TableComponentProps<T>): JSX.Element {
  return (
    <StyledTableContainer>
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
                const rawValue = item[column.id as keyof T];
                // Check if value is renderable or needs to be converted
                const value = (typeof rawValue === 'string' || typeof rawValue === 'number')
                  ? rawValue
                  : JSON.stringify(rawValue); // Convert non-renderable types to string
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
